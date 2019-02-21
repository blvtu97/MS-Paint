﻿using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimplePaint.Model
{
    public class Circle : Ellipse
    {
        public int Diameter { get; set; }

        public Circle()
        {
            Name = "Circle";
        }

        protected override GraphicsPath GraphicsPath
        {
            get
            {
                GraphicsPath path = new GraphicsPath();

                Diameter = ((End.X - Begin.X) + (End.Y - Begin.Y)) / 2;

                path.AddEllipse(new System.Drawing.Rectangle(Begin.X, Begin.Y, Diameter, Diameter));

                return path;
            }
        }

        public override object Clone()
        {
            return new Circle
            {
                Begin = Begin,
                End = End,
                IsSelected = IsSelected,
                Name = Name,
                Color = Color,
                LineWidth = LineWidth,
                Diameter = Diameter
            };
        }

        public override void Draw(Graphics graphics)
        {
            using (GraphicsPath path = GraphicsPath)
            {
                if (!Fill)
                {
                    using (Pen pen = new Pen(Color, LineWidth) { DashStyle = DashStyle })
                    {
                        graphics.DrawPath(pen, path);
                    }
                }
                else
                {
                    using (Brush brush = new SolidBrush(Color))
                    {
                        graphics.FillPath(brush, path);
                    }
                }
            }
        }

        public override bool IsHit(Point point)
        {
            bool res = false;
            using (GraphicsPath path = GraphicsPath)
            {
                if (!Fill)
                {
                    using (Pen pen = new Pen(Color, LineWidth + 3))
                    {
                        res = path.IsOutlineVisible(point, pen);
                    }
                }
                else
                {
                    res = path.IsVisible(point);
                }
            }
            return res;
        }

        public override void Move(Point distance)
        {
            Begin = new Point(Begin.X + distance.X, Begin.Y + distance.Y);
            End = new Point(End.X + distance.X, End.Y + distance.Y);
        }
    }
}
