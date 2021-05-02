using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACE_lib.Args;
using ACE_lib.Content.Canvases;
using ACE_lib.Regions;
using ACE_lib.Vectors;

namespace ACE_lib.Content.Entities
{
	//Add: Save & Load
	public class AnimationEnt2 : IContent2
	{
		public AnimationEnt2()
		{ 
			this.pFrameList = new List<SprEnt2>();
			this.pIndex = 0;

			this.pSize = new Vec2i();
			this.pPos = new Vec2d();
		}
		public AnimationEnt2(int xSize, int ySize) : this() => this.pSize.Init(xSize, ySize);
		public AnimationEnt2(int xSize, int ySize, double xPos, double yPos) : this(xSize, ySize) => this.pPos.Init(xPos, yPos);

		public AnimationEnt2(IConnectable2 Parent) : this() => Parent.AddConnection(this);
		public AnimationEnt2(IConnectable2 Parent, int xSize, int ySize) : this(xSize, ySize) => Parent.AddConnection(this);
		public AnimationEnt2(IConnectable2 Parent, int xSize, int ySize, double xPos, double yPos) : this(xSize, ySize, xPos, yPos) => Parent.AddConnection(this);

		public AnimationEnt2(Vec2i Size) : this(Size.X, Size.Y) { }
		public AnimationEnt2(Vec2i Size, Vec2i Pos) : this(Size.X, Size.Y, Pos.X, Pos.Y) { }
		public AnimationEnt2(Vec2i Size, Vec2d Pos) : this(Size.X, Size.Y, Pos.X, Pos.Y) { }
		public AnimationEnt2(Reg2 Region) : this(Region.GetSize(), Region.GetPosition()) { }
		public AnimationEnt2(IConnectable2 Parent, Vec2i Size) : this(Parent, Size.X, Size.Y) { }
		public AnimationEnt2(IConnectable2 Parent, Vec2i Size, Vec2i Pos) : this(Parent, Size.X, Size.Y, Pos.X, Pos.Y) { }
		public AnimationEnt2(IConnectable2 Parent, Vec2i Size, Vec2d Pos) : this(Parent, Size.X, Size.Y, Pos.X, Pos.Y) { }
		public AnimationEnt2(IConnectable2 Parent, Reg2 Region) : this(Parent, Region.GetSize(), Region.GetPosition()) { }

		public int CurrentFrameIndex 
		{
			get => this.pIndex;
			set
			{
				if (value < 0 || value >= this.FrameCount) throw new IndexOutOfRangeException("Frame index too big!");

				int old = this.pIndex;

				this.pIndex = value;

				this.OnFrameIndexChange?.Invoke(this, new OnValueChangedArgs<int> { OldValue = old, NewValue = this.pIndex });
			}
		}
		public int FrameCount { get => this.pFrameList.Count; }

		public char this[Vec2i Index] { get => this.GetAt(Index); set => this.SetAt(value, Index); }
		public char this[int x, int y] { get => this.GetAt(x, y); set => this.SetAt(value, x, y); }

		public event EventHandler OnFrameAdded;
		public event EventHandler OnFrameDropped;

		public event EventHandler OnColorsCleared;
		public event EventHandler OnContentCleared;

		public event EventHandler OnAllFramesDropped;

		public event EventHandler<OnValueChangedArgs<int>> OnFrameIndexChange;

		public event EventHandler<OnModifiedArgs<char>> OnContentModified;
		public event EventHandler<OnModifiedArgs<ConsoleColor>> OnColorsModified;

		public event EventHandler<OnValueChangedArgs<Vec2d>> OnPositionChanged;
		public event EventHandler<OnValueChangedArgs<Vec2i>> OnSizeChanged;
		public event EventHandler<OnValueChangedArgs<Reg2>> OnRegionChanged;

		public event EventHandler OnPreAppend;
		public event EventHandler OnPostAppend;

		public event EventHandler OnPreColorDraw;
		public event EventHandler OnPostColorDraw;

		public void AddFrame(bool AdjustCurrentFrameIndexToLast = false)
		{
			this.pFrameList.Add(new SprEnt2(this.GetSize()));

			this.OnFrameAdded?.Invoke(this, new EventArgs());

			if (AdjustCurrentFrameIndexToLast) this.CurrentFrameIndex = this.FrameCount - 1;
		}
		public void AddFrame(int FrameCount, bool AdjustCurrentFrameIndexToLast = false)
		{
			for (int i = 0; i < FrameCount; ++i)
			{
				this.AddFrame();
			}

			if (AdjustCurrentFrameIndexToLast) this.CurrentFrameIndex = this.FrameCount - 1;
		}

		public void AddFrameAndCopyContent(IContent2 Content, bool AdjustCurrentFrameIndexToLast = false)
		{
			this.AddFrame(AdjustCurrentFrameIndexToLast);

			this.CopyContentToFrame(Content, this.FrameCount - 1);

			if (AdjustCurrentFrameIndexToLast) this.CurrentFrameIndex = this.FrameCount - 1;
		}

		public void CopyContentToFrame(IContent2 Content, int FrameIndex)
		{
			Vec2d tPos = Content.GetPosition();
			int tFrame = this.CurrentFrameIndex;

			Content.SetPosition(0, 0);
			this.CurrentFrameIndex = FrameIndex;

			Content.AppendTo(this);

			Content.SetPosition(tPos);
			this.CurrentFrameIndex = tFrame;
		}

		public void DropFrame(int FrameIndex)
		{
			this.pFrameList.RemoveAt(FrameIndex);

			this.CurrentFrameIndex = 0;
			this.OnFrameDropped?.Invoke(this, new EventArgs());
		}

		public void DropAllFrames()
		{
			this.pFrameList.Clear();

			this.CurrentFrameIndex = 0;
			this.OnAllFramesDropped?.Invoke(this, new EventArgs());
		}

		public void SetAtFrame(int FrameIndex, char el, int x, int y)
		{
			if (FrameIndex < 0 || FrameIndex >= this.pFrameList.Count || this.pCheckIndex(x, y)) throw new IndexOutOfRangeException();
			//Redundtant / not neccessary
			//this.pUpdateFrame(FrameIndex);

			this.pFrameList[FrameIndex].SetAt(el, x, y);

			this.OnContentModified?.Invoke(this, new OnModifiedArgs<char> { Index = new Vec2i(x, y), ValueAt = el });
		}
		public void SetAtFrame(int FrameIndex, char el, Vec2i Index) => this.SetAtFrame(FrameIndex, el, Index.X, Index.Y);

		public void SetColorAtFrame(int FrameIndex, ConsoleColor Col, int x, int y)
		{
			if (FrameIndex < 0 || FrameIndex >= this.pFrameList.Count || this.pCheckIndex(x, y)) throw new IndexOutOfRangeException();
			//Redundtant / not neccessary
			//this.pUpdateFrame(FrameIndex);

			this.pFrameList[FrameIndex].SetColorAt(Col, x, y);

			this.OnColorsModified?.Invoke(this, new OnModifiedArgs<ConsoleColor> { Index = new Vec2i(x, y), ValueAt = Col });
		}
		public void SetColorAtFrame(int FrameIndex, ConsoleColor Col, Vec2i Index) => this.SetColorAtFrame(FrameIndex, Col, Index.X, Index.Y);

		public char GetAtFrame(int FrameIndex, int x, int y)
		{
			if (FrameIndex < 0 || FrameIndex >= this.pFrameList.Count || this.pCheckIndex(x, y)) throw new IndexOutOfRangeException();

			//Redundtant / not neccessary
			//this.pUpdateFrame(FrameIndex);

			return this.pFrameList[FrameIndex].GetAt(x, y);
		}
		public char GetAtFrame(int FrameIndex, Vec2i Index) => this.GetAtFrame(FrameIndex, Index.X, Index.Y);

		public ConsoleColor GetColorAtFrame(int FrameIndex, int x, int y)
		{
			if (FrameIndex < 0 || FrameIndex >= this.pFrameList.Count || this.pCheckIndex(x, y)) throw new IndexOutOfRangeException();

			//Redundtant / not neccessary
			//this.pUpdateFrame(FrameIndex);

			return this.pFrameList[FrameIndex].GetColorAt(x, y);
		}
		public ConsoleColor GetColorAtFrame(int FrameIndex, Vec2i Index) => this.GetColorAtFrame(FrameIndex, Index.X, Index.Y);

		public void ClearAtFrame(int FrameIndex, char ClearWith)
		{
			if (FrameIndex < 0 || FrameIndex >= this.pFrameList.Count) throw new IndexOutOfRangeException();

			this.pFrameList[FrameIndex].Clear(ClearWith);

			this.OnContentCleared?.Invoke(this, new EventArgs());
		}
		public void ClearColorsAtFrame(int FrameIndex, ConsoleColor ClearWith)
		{
			if (FrameIndex < 0 || FrameIndex >= this.pFrameList.Count) throw new IndexOutOfRangeException();

			this.pFrameList[FrameIndex].ClearColors(ClearWith);

			this.OnColorsCleared?.Invoke(this, new EventArgs());
		}

		public void SetAt(char el, int x, int y) => this.SetAtFrame(this.CurrentFrameIndex, el, x, y);
		public void SetAt(char el, Vec2i Index) => this.SetAt(el, Index.X, Index.Y);

		public void SetColorAt(ConsoleColor Col, int x, int y) => this.SetColorAtFrame(this.CurrentFrameIndex, Col, x, y);
		public void SetColorAt(ConsoleColor Col, Vec2i Index) => this.SetColorAt(Col, Index.X, Index.Y);

		public char GetAt(int x, int y) => this.GetAtFrame(this.CurrentFrameIndex, x, y);
		public char GetAt(Vec2i Index) => this.GetAt(Index.X, Index.Y);

		public ConsoleColor GetColorAt(int x, int y) => this.GetColorAtFrame(this.CurrentFrameIndex, x, y);
		public ConsoleColor GetColorAt(Vec2i Index) => this.GetColorAt(Index.X, Index.Y);

		public void Clear(char ClearWith) => this.ClearAtFrame(this.CurrentFrameIndex, ClearWith);
		public void ClearColors(ConsoleColor ClearWith) => this.ClearColorsAtFrame(this.CurrentFrameIndex, ClearWith);

		public Vec2i GetSize() => this.pSize.Clone() as Vec2i;
		public Vec2d GetPosition() => this.pPos.Clone() as Vec2d;
		public Reg2 GetRegion() => new Reg2(this.pSize, this.pPos);

		public void SetPosition(double xPos, double yPos)
		{
			Vec2d Old = this.GetPosition();

			this.pPos.Init(xPos, yPos);

			this.OnPositionChanged?.Invoke(this, new OnValueChangedArgs<Vec2d> { OldValue = Old, NewValue = this.GetPosition() });
		}
		public void SetPosition(Vec2i Pos) => this.SetPosition(Pos.X, Pos.Y);
		public void SetPosition(Vec2d Pos) => this.SetPosition(Pos.X, Pos.Y);

		public void SetSize(int xSize, int ySize)
		{
			Vec2i Old = this.GetSize();

			this.pSize.Init(xSize, ySize);
			this.pUpdateFrames();

			this.OnSizeChanged?.Invoke(this, new OnValueChangedArgs<Vec2i> { OldValue = Old, NewValue = this.GetSize() });
		}

		public void SetSize(Vec2i Size) => this.SetSize(Size.X, Size.Y);
		public void ResizeTo(int xSize, int ySize) => this.SetSize(xSize, ySize);
		public void ResizeTo(Vec2i Size) => this.SetSize(Size.X, Size.Y);

		public void SetRegion(int xSize, int ySize, double xPos, double yPos)
		{
			Reg2 Old = this.GetRegion();

			this.SetSize(xSize, ySize);
			this.SetPosition(xPos, yPos);

			this.OnRegionChanged?.Invoke(this, new OnValueChangedArgs<Reg2> { OldValue = Old, NewValue = this.GetRegion() });
		}
		public void SetRegion(int xSize, int ySize) => this.SetRegion(xSize, ySize, this.pPos.X, this.pPos.Y);
		public void SetRegion(Vec2i Size) => this.SetRegion(Size.X, Size.Y);
		public void SetRegion(Vec2i Size, Vec2i Pos) => this.SetRegion(Size.X, Size.Y, Pos.X, Pos.Y);
		public void SetRegion(Vec2i Size, Vec2d Pos) => this.SetRegion(Size.X, Size.Y, Pos.X, Pos.Y);
		public void SetRegion(Reg2 Reg) => this.SetRegion(Reg.GetSize(), Reg.GetPosition());

		public void AppendTo(IContent2 Content)
		{
			this.OnPreAppend?.Invoke(this, new EventArgs());

			this.pForEachTransposed(Content.GetSize(), (xParent, yParent, xMe, yMe) => {

				Content.SetAt(this.GetAt(xMe, yMe), xParent, yParent);
				Content.SetColorAt(this.GetColorAt(xMe, yMe), xParent, yParent);

			});

			this.OnPostAppend?.Invoke(this, new EventArgs());
		}
		public void AppendTo(IModifiable2<char> Content)
		{
			this.OnPreAppend?.Invoke(this, new EventArgs());

			this.pForEachTransposed(Content.GetSize(), (xParent, yParent, xMe, yMe) => {

				Content.SetAt(this.GetAt(xMe, yMe), xParent, yParent);

			});

			this.OnPostAppend?.Invoke(this, new EventArgs());
		}
		public void DrawColorsToCan(Can2 Canvas)
		{
			this.OnPreColorDraw?.Invoke(this, new EventArgs());

			this.pForEachTransposed(Canvas.GetSize(), (xParent, yParent, xMe, yMe) => {

				Canvas.DrawColorAt(this.GetColorAt(xMe, yMe), xParent, yParent);

			});

			this.OnPostColorDraw?.Invoke(this, new EventArgs());
		}

		public void SaveAnimationToFile(BinaryWriter bw)
		{
			bw.Write(this.FrameCount);
			for (int i = 0; i < this.FrameCount; ++i)
			{
				this.pFrameList[i].SaveSpriteToFile(bw);
			}
		}

		public static AnimationEnt2 LoadAnimationFromFile(BinaryReader br)
		{
			int count = br.ReadInt32();
			SprEnt2 temp = SprEnt2.LoadSpriteFromFile(br);

			AnimationEnt2 result = new AnimationEnt2(temp.GetSize());
			result.AddFrameAndCopyContent(temp);

			for (int i = 1; i < count; ++i)
			{
				result.AddFrameAndCopyContent(SprEnt2.LoadSpriteFromFile(br));
			}

			result.CurrentFrameIndex = 0;

			return result;
		}
		public static AnimationEnt2 LoadAnimationFromFile(BinaryReader br, double xPos, double yPos)
		{
			AnimationEnt2 result = LoadAnimationFromFile(br);
			result.SetPosition(xPos, yPos);
			return result;
		}
		public static AnimationEnt2 LoadAnimationFromFile(BinaryReader br, IConnectable2 Parent, double xPos, double yPos)
		{
			AnimationEnt2 result = LoadAnimationFromFile(br, xPos, yPos);
			Parent.AddConnection(result);
			return result;
		}

		public static AnimationEnt2 LoadAnimationFromFile(BinaryReader br, IConnectable2 Parent) => LoadAnimationFromFile(br, Parent, 0, 0);

		public static AnimationEnt2 LoadAnimationFromFile(BinaryReader br, Vec2i Position) => LoadAnimationFromFile(br, Position.X, Position.Y);
		public static AnimationEnt2 LoadAnimationFromFile(BinaryReader br, Vec2d Position) => LoadAnimationFromFile(br, Position.X, Position.Y);

		public static AnimationEnt2 LoadAnimationFromFile(BinaryReader br, IConnectable2 Parent, Vec2i Position) => LoadAnimationFromFile(br, Parent, Position.X, Position.Y);
		public static AnimationEnt2 LoadAnimationFromFile(BinaryReader br, IConnectable2 Parent, Vec2d Position) => LoadAnimationFromFile(br, Parent, Position.X, Position.Y);

		private bool pCheckIndex(int x, int y) => (x < 0 || y < 0 || x >= this.pSize.X || y >= this.pSize.Y);

		private void pUpdateFrames()
		{
			for (int i = 0; i < this.FrameCount; ++i)
			{
				this.pUpdateFrame(i);
			}
		}
		private void pUpdateFrame(int FrameIndex)
		{
			if (this.pFrameList[FrameIndex].GetSize() != this.pSize)
			{
				this.pFrameList[FrameIndex].ResizeTo(this.pSize);
			}
		}

		private void pForEachTransposed(Vec2i ParentSize, Action<int, int, int, int> a)
		{
			Vec2i Index = new Vec2i();

			for (int y = 0; y < this.pSize.Y; ++y)
			{
				for (int x = 0; x < this.pSize.X; ++x)
				{
					Index.X = (int)this.pPos.X + x;
					Index.Y = (int)this.pPos.Y + y;

					if (Index.X < 0 || Index.Y < 0 || Index.X >= ParentSize.X || Index.Y >= ParentSize.Y) continue;

					a.Invoke(Index.X, Index.Y, x, y);
				}
			}
		}

		private IList<SprEnt2> pFrameList;
		private int pIndex = 0;

		private Vec2i pSize;
		private Vec2d pPos;
	}
}
