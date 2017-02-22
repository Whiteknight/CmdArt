A Screen will represent the UI and will conform to the Console window
    A Screen will have an IPixelBuffer which will be the size of the ConsoleWindow
    A Screen will have a ScreenRenderer, to render the buffer to the console window
    A user can write directly to the Screen's buffer or can interact through a window
    A Screen will have zero or more Windows, which each correspond to a Region of the Screen and do not overlap

A Window will represent a renderable region of the Screen
    A Window will have a reference to the Screen and Screen.Buffer
    A Window will have its own IPixelBuffer, which can be projected onto the Screen by calling Window.Render()
    A Window buffer May be larger than the region of the screen it represents. The Window will project a region of the buffer to a region on the Screen.Buffer
    A Window can change its source location, giving the background the appearance of moving
    A Window will hold zero or more Filters, which will take the source and destination buffers and locations and allow transformations or decorations

A CircularPixelBuffer will be an IPixelBuffer with circular behavior. Writing to a new line will overwrite an old line and reset the origin
    This will allow text scrolling without using an infinitely-sized buffer

Process to render something:

1) Create an IRenderable and buffer
2) renderable.Render(buffer) Can be Screen.Buffer for direct writes, but assume a standalone buffer for now.
    renderable.Render(buffer) should render starting at (0,0), we can use windows and projections to move these things around on the screen
3) window = screen.CreateWindow(location1, size) checks for overlaps and creates the new window
4) window.SetBuffer(buffer) Only needs to be set once per buffer
5) window.SetSourceRegion(location2) sets the region of the source buffer to use, uses the size of the window
6) window.Render() projects from its internal buffer to window.Screen.Buffer
6) screen.Render() pushes changes to the screen
    alternatively something like screen.Render(all: true) would render all Windows first, and then .Render() the screen

