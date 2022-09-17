using System;
using System.Drawing;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
using static AGAIN.GameVar;

namespace AGAIN
{
    class MainProgram
    {
        static void Main()
        {
            Stopwatch s = new Stopwatch();
            NativeWindowSettings nws = NativeWindowSettings.Default;
            nws.Title = "no title";
            nws.APIVersion = Version.Parse("3.3");
            nws.Location = null;
            GameWindowSettings gws = GameWindowSettings.Default;
            gws.UpdateFrequency = 0.0;
            GameWindow gw = new GameWindow(gws, nws);
            gw.Load += () =>
            {
                GameWatch.Start();
                var vao = GL.GenVertexArray();
                var vbo = GL.GenBuffer();
                GL.UseProgram(Shader.DefaultShader);
                GL.BindVertexArray(vao);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                for (int i = 0; i < 3; i++)
                {
                    GL.EnableVertexAttribArray(i);
                }
                Vector2[] V = new[] { Vector2.Zero, new Vector2(1, -1), Vector2.One };
                RenderObj T = new RenderObj(V, new Color4[] { Color4.White, Color4.Red, Color4.Black }, 2);
                GObject O = new GObject(T);
                Vector2[] V1 = new[] { new Vector2(1, -1), new Vector2(1, 1), -Vector2.One };
                RenderObj T1 = new RenderObj(V1, new Color4[] { Color4.Black, Color4.Black, Color4.Black }, 1);
                GObject O1 = new GObject(T1);
                Vector2[] V2 = new[] { new Vector2(1, -1), new Vector2(-1, 1), -Vector2.One };
                RenderObj T2 = new RenderObj(V2, new Color4[] { Color4.Aqua, Color4.Aqua, Color4.Aqua }, 2);
                GObject O2 = new GObject(T2);
            };
            gw.UpdateFrame += (feventagrs) =>
            {
                if (gw.IsKeyDown(Keys.Escape)) { gw.Close(); }
                if (gw.IsKeyPressed(Keys.R))
                {

                }
                if (gw.IsKeyPressed(Keys.A))
                {
                    try
                    {
                        ObjCollection[0].Dispose();
                    }
                    catch { }
                    
                }
                if (gw.IsKeyPressed(Keys.Escape)) { gw.Close(); }
                
                
            };
            gw.RenderFrame += (feventagrs) =>
            {
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.ClearColor(Color.Magenta);

                for (int i = 0; i < ObjCollection.Count; i++)
                {
                    var item = ObjCollection[i].RO.Layer;
                    var currentIndex = i;

                    while (currentIndex > 0 && ObjCollection[currentIndex - 1].RO.Layer < item)
                    {
                        currentIndex--;
                    }
                    ObjCollection.Insert(currentIndex, ObjCollection[i]);
                    ObjCollection.RemoveAt(i + 1);
                }
                for (int i = ObjCollection.Count - 1; i >= 0; i--)
                {
                    ObjCollection[i].Render(ObjCollection);
                }
                gw.SwapBuffers();
                Console.WriteLine(GameWatch.ElapsedMilliseconds);
            };

            gw.Run();
        }
    }
}