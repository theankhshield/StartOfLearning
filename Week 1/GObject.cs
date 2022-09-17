using OpenTK.Mathematics;
using System.Collections.Generic;
using System;
using static AGAIN.GameVar;

namespace AGAIN
{
    public class GObject: IDisposable
    {

        public RenderObj RO;
        private bool Disposed;
        public readonly int Id;
        public Vector2 Position;
        public float Rotation;
        public GObject(RenderObj rO, Vector2 position, float rotation, bool Add = true)
        {
            Disposed = false;
            Position = position;
            Rotation = rotation;
            RO = rO;
            for (int i = 0; i <= ObjCollection.Count; i++)
            {
                var none = true;
                foreach (var item in ObjCollection)
                {
                    if (item.Id == i) { none = false; }
                }
                if (none)
                {
                    Id = i;
                    break;
                }
            }
            if (Add) {ObjCollection.Add(this); }
        }
        public GObject(RenderObj rO, bool Add = true) : this(rO, Vector2.Zero, 0, Add) { }
        public void Render(List<GObject> List)
        {
            if (Disposed) { List.Remove(this); }
            else
            {
                RO.Render();
            }
        }
        public void Dispose()
        {
            Disposed = true;
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}