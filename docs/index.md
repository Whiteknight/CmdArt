# CmdArt
A futile, misguided attempt to bring art to the CMD prompt.

## Overview

CmdArt provides some abstractions and nice features for working with text in a CMD console window. The primary feature of CmdArt is that it provides an algorithm for displaying images in the console. These images are constrained by the limitations of CMD window (very low "pixel" density, 16 color palette, etc). Basically, this library lets you add a little bit of attractiveness and flair to an existing console program.

## Usage

### Basics

First, create a `TerminalScreen` object. 

    var screen = new TerminalScreen();

The `screen` contains buffers and rendering logic. You can write characters and colors to the buffer, and then render the buffer to the CMD window:

    screen.Render();

### Images

To display an image in the console, you load the `Bitmap` object into your program, downsample the image and do the color conversions, and finally render the image to a buffer.

    var image = CmdArt.Images.Image.BuildFromImageFile(fileName, new Size(100, 50));
    image.RenderTo(screen.Buffer);
    screen.Render();

### Windowing

A `TerminalScreenWindow` object allows you to project from a region in one buffer to a region on the screen buffer. This is useful if you want to show only a portion of an image buffer, or if you want the image to render somewhere besides in the upper-left corner of the screen.

    // Create a window at position (5,5) on the screen, with size 20x16
    var window = screen.CreateNewWindow(new Region(5, 5, 20, 16));

    // Set a source buffer in the window, big enough to hold the image
    // We want to focus on the region of the window at point (7, 4), where the size is
    // still 20x16
    var buffer = screen.BufferFactory.Create(image.Size);
    window.SetSourceBuffer(buffer, new Location(7, 4));

    // Render the image to the Window's buffer, then render the screen to the Console
    // including the window's buffer
    image.RenderTo(window.SourceBuffer);
    screen.Render(includeWindows: true);

You can create many windows on your screen but they may not overlap with one another. 

## Contributing

Let's face it, the DOS prompt isn't a place where anybody is going to write a rich user interface with all sorts of graphics and decorations and user input controls. That said, the buffering system offered in CmdArt could easily be used for other purposes besides really crappy image rendering. If you have some ideas to make CmdArt better or ways to add features of real value to the library without going overboard with unnecessary and unjustified complexity, please create a fork and open a pull request. I'm always happy to take contributions.

## To Be Done

There are a few additional features that I think might be good to add to this library but I recognize that this entire project is a waste of everybody's time. I'll work on it when the mood strikes me, and I'll definitely respond to bug reports or feature requests if there are going to be any actual users of this library. Otherwise, don't expect big changes and feverishly active development here.
