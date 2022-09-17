using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AGAIN
{
    public struct RenderObj
    {
        public Vector2[] RenderShape;
        public Color4[] Color;
        public Vector2[] TextureCord;
        public int TextureID;
        public float Layer;
        public RenderObj(Vector2[] RenderShape, Color4[] Color, Vector2[] TexCord, float Layer, int TexID = 0)
        {
            int i = RenderShape.Length - RenderShape.Length % 3;
            this.RenderShape = new Vector2[i];
            for (int t = 0; t < i; t++)
            {
                this.RenderShape[t] = RenderShape[t];
            }
            if (Color.Length == 1)
            {
                this.Color = new Color4[i];
                for (int t = 0; t < this.Color.Length; t++)
                {
                    this.Color[t] = Color[0];
                }
            }
            else
            {
                this.Color = Color;
            }
            this.Layer = Layer;
            TextureID = TexID;
            if (TexID == 0)
            {
                TextureCord = TexCord;
            }
            else
            {
                TextureCord = new Vector2[] { };
            }
            
        }

        public RenderObj(Vector2[] RS, Color4[] C, float L) : this(RS, C, new Vector2[] { }, L) { }
        public RenderObj(Vector2[] RS, Color4 C, float L) : this(RS, new Color4[] { C }, new Vector2[] { }, L) { }

        private int Size => RenderShape.Length * 4 ;
        public void Render()
        {
            GL.BufferData(BufferTarget.ArrayBuffer, Size * 8, IntPtr.Zero, BufferUsageHint.DynamicDraw);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, Size * 2, RenderShape);
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(Size * 2), Size * 4, Color);
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(6 * Size), 2 * Size, TextureCord);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, 0);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, sizeof(float) * 4, 2 * Size);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, 6 * Size);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

        public static RenderObj RectangleTR(Vector2 TRPos, Vector2 Size, float Layer)
        {
            Vector2[] RS =
            {
                TRPos, TRPos-Size, TRPos-new Vector2(Size.X,0),
                TRPos, TRPos-Size, TRPos-new Vector2(0,Size.Y)
            };
            return new RenderObj(RS, Color4.White, Layer);
        }
        public static RenderObj RectangleCT(Vector2 CTPos, Vector2 Size, float Layer)
        {
            return RectangleTR(CTPos + Size / 2, Size, Layer);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RenderObj)) { return false; }
            return Equals((RenderObj)obj);
        }
        public bool Equals(RenderObj obj)
        {
            
            return Enumerable.SequenceEqual(obj.RenderShape, RenderShape)
                   && Enumerable.SequenceEqual(obj.Color, Color)
                   && Enumerable.SequenceEqual(obj.TextureCord, TextureCord)
                   && Layer == obj.Layer;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RenderShape, Color, TextureCord, Layer);
        }
    }
}