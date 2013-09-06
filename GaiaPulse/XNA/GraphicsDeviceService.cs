using System;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

#pragma warning disable 67

namespace GaiaPulse.XNA
{
    internal class GraphicsDeviceService : IGraphicsDeviceService
    {
        static GraphicsDeviceService _singletonInstance;

        static int _referenceCount;

        PresentationParameters _parameters;

        public GraphicsDevice GraphicsDevice
        {
            get { return _graphicsdevice; }
        }

        GraphicsDevice _graphicsdevice;

        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;

        private GraphicsDeviceService(IntPtr windowHandle, int width, int height)
        {
            _parameters = new PresentationParameters();
            _parameters.BackBufferWidth = Math.Max(width, 1);
            _parameters.BackBufferHeight = Math.Max(height, 1);
            _parameters.BackBufferFormat = SurfaceFormat.Color;
            _parameters.DepthStencilFormat = DepthFormat.Depth24;
            _parameters.DeviceWindowHandle = windowHandle;
            _parameters.PresentationInterval = PresentInterval.Immediate;
            _parameters.IsFullScreen = false;

            _graphicsdevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, _parameters);
        }

        public static GraphicsDeviceService AddRef(IntPtr windowHandle, int width, int height)
        {
            if (Interlocked.Increment(ref _referenceCount) == 1)
            {
                _singletonInstance = new GraphicsDeviceService(windowHandle, width, height);
            }

            return _singletonInstance;
        }

        public void Release(bool disposing)
        {
            if (Interlocked.Decrement(ref _referenceCount) == 0)
            {
                if (disposing)
                {
                    if (DeviceDisposing != null)
                        DeviceDisposing(this, EventArgs.Empty);

                    GraphicsDevice.Dispose();
                }

                _graphicsdevice = null;
            }
        }

        public void ResetDevice(int width, int height)
        {
            if (DeviceResetting != null)
                DeviceResetting(this, EventArgs.Empty);

            _parameters.BackBufferWidth = Math.Max(_parameters.BackBufferWidth, width);
            _parameters.BackBufferHeight = Math.Max(_parameters.BackBufferHeight, height);

            _graphicsdevice.Reset(_parameters);

            if (DeviceReset != null)
                DeviceReset(this, EventArgs.Empty);
        }
    }
}