using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE_lib2.Content.Props
{
	public sealed class TitleProps
	{
		public TitleProps() 
		{
			this.Visible = false;
		}
		public TitleProps(string Title)
		{
			this.Title = Title;

			this.TitleColor = ConsoleColor.Gray;
			this.DecoratorColor = ConsoleColor.Gray;

			this.Visible = true;
		}
		public TitleProps(string Title, string LeftDecorator, string RightDecorator) : this(Title)
		{
			this.LeftDecorator = LeftDecorator;
			this.RightDecorator = RightDecorator;

			this.DecoratorColor = ConsoleColor.Gray;
		}
		public TitleProps(string Title, string LeftDecorator, string RightDecorator, ConsoleColor TitleColor, ConsoleColor DecoratorColor) : this(Title, LeftDecorator, RightDecorator)
		{
			this.TitleColor = TitleColor;
			this.DecoratorColor = DecoratorColor;
		}

		public string LeftDecorator { get; set; }
		public string RightDecorator { get; set; }
		public string Title { get; set; }

		public ConsoleColor TitleColor { get; set; }
		public ConsoleColor DecoratorColor { get; set; }

		public bool Visible { get; set; }
	}
}
