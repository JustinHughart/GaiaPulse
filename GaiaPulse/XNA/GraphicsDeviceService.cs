using System;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

#pragma warning disable 67

namespace GaiaPulse.XNA
{
    class GraphicsDeviceService: IGraphicsDeviceService
    {
        static GraphicsDeviceService SingletonInstance;

        static int ReferenceCount;

        PresentationParameters parameters;

        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsdevice; }
        }

        GraphicsDevice graphicsdevice;

        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;

        GraphicsDeviceService(IntPtr WindowHandle, int Width, int Height)
        {
            parameters = new PresentationParameters();
            parameters.BackBufferWidth = Math.Max(Width, 1);
            parameters.BackBufferHeight = Math.Max(Height, 1);
            parameters.BackBufferFormat = SurfaceFormat.Color;
            parameters.DepthStencilFormat = DepthFormat.Depth24;
            parameters.DeviceWindowHandle = WindowHandle;
            parameters.PresentationInterval = PresentInterval.Immediate;
            parameters.IsFullScreen = false;

            graphicsdevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, parameters);
        }

        public static GraphicsDeviceService AddRef(IntPtr WindowHandle, int Width, int Height)
        {
            if (Interlocked.Increment(ref ReferenceCount) == 1)
            {
                SingletonInstance = new GraphicsDeviceService(WindowHandle, Width, Height);
            }

            return SingletonInstance;
        }

        public void Release(bool disposing)
        {
           if (Interlocked.Decrement(ref ReferenceCount) == 0)
            {
                if (disposing)
                {
                    if (DeviceDisposing != null)
                        DeviceDisposing(this, EventArgs.Empty);

                    GraphicsDevice.Dispose();
                }

                graphicsdevice = null;
            }
        }

        public void ResetDevice(int Width, int Height)
        {
            if (DeviceResetting != null)
                DeviceResetting(this, EventArgs.Empty);

            parameters.BackBufferWidth = Math.Max(parameters.BackBufferWidth, Width);
            parameters.BackBufferHeight = Math.Max(parameters.BackBufferHeight, Height);

            graphicsdevice.Reset(parameters);

            if (DeviceReset != null)
                DeviceReset(this, EventArgs.Empty);
        }
    }
}
