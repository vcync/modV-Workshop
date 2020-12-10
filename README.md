<h1>modV Workshop</h1>

![modV workshop](media/20201124_modV_workshop_cover_838.jpg)

> By using the open source software [modV](https://modv.vcync.gl/) you will be able to create audio reactive visualisations by combining different kinds of visual modules (2D, [Interactive Shader Format](https://isf.video/) and WebGL Shader) with any kind of audio input like a microphone or your favorite music. In the end you will know all the basics in order to do your first live performance as a VJ.

---

- [Prepare before the Workshop](#prepare-before-the-workshop)
  - [modV setup](#modv-setup)
  - [Audio Routing](#audio-routing)
  - [Download / clone the repository](#download--clone-the-repository)
- [1. History of modV](#1-history-of-modv)
  - [Who is behind modV?](#who-is-behind-modv)
  - [Where is modV being used?](#where-is-modv-being-used)
- [2. How to use modV](#2-how-to-use-modv)
  - [User Interface](#user-interface)
    - [Output Window](#output-window)
  - [Menu](#menu)
  - [Rearrange the panel layout](#rearrange-the-panel-layout)
  - [Add a module to a group](#add-a-module-to-a-group)
    - [Text](#text)
  - [Change group preferences](#change-group-preferences)
  - [Use a filter: Pixelate](#use-a-filter-pixelate)
  - [Change module preferences](#change-module-preferences)
  - [Add another group and rename it](#add-another-group-and-rename-it)
  - [Save / Load a preset](#save--load-a-preset)
- [Breakout Session A](#breakout-session-a)
  - [Add images into modV](#add-images-into-modv)
  - [Add modules into modV](#add-modules-into-modv)
    - [Module licence](#module-licence)
    - [Add ISF shader into modV](#add-isf-shader-into-modv)
    - [Use blending to make logo visible](#use-blending-to-make-logo-visible)
- [3. Use Audio in modV](#3-use-audio-in-modv)
  - [Audio processing in modV](#audio-processing-in-modv)
  - [Audio routing](#audio-routing-1)
  - [Audio reactive visuals](#audio-reactive-visuals)
- [Breakout Session B](#breakout-session-b)
- [4. Techniques to get certain effects](#4-techniques-to-get-certain-effects)
  - [Tunnel effect](#tunnel-effect)
      - [Module order](#module-order)
  - [Background fade](#background-fade)
      - [Module order](#module-order-1)
  - [Hue rotation for trailing colors](#hue-rotation-for-trailing-colors)
      - [Module order](#module-order-2)
  - [Text mask](#text-mask)
    - [Group 1](#group-1)
      - [Group settings](#group-settings)
      - [Module order](#module-order-3)
    - [Group 2](#group-2)
      - [Module order](#module-order-4)
  - [Liquid text](#liquid-text)
- [Breakout Session C](#breakout-session-c)
- [5. Use modV for a live performance](#5-use-modv-for-a-live-performance)

---


# Prepare before the Workshop

If you have problems to prepare for the workshop, please make sure to reach out to us: [Tim Pietrusky](https://nerddis.co) or [Sam Wray](https://2xaa.fm)!

## modV setup

Install the [latest version of modV](https://github.com/vcync/modV/releases/latest). 

When you start modV, two windows should open: The **main window** and the **output window**. 

![modV main window](media/20201204_modv_main_window.png)
*modV main window*

![modV output window](media/20201204_modv_output_window.png)
*modV output window*


## Audio Routing

In order to get audio into modV to be able to visualize it, you need to [follow one of these guides for your operating system](https://modv.vcync.gl/v3/guide/#audio-routing). 

When everything works out, you can select your Audio Input under **Input Device Config**

![modV Audio Input Config](media/20201204_modV_audio_input_config.png)
*Audio Input Config*

If it doesn't work out, you can still use the microphone of your computer!

## Download / clone the repository

In order to get all files that are needed for the workshop, please [download](https://github.com/vcync/modV-Workshop/archive/main.zip) or clone the repository to your computer. 

The important folders are:

* [modules](/modules): Example modules to extend modV
* [presets](/presets): Configurations that will be used during the workshop

---

# 1. History of modV

In this workshop we are using modV 3.x, the latest version of modV that comes as a standalone desktop application. With modV, you can combine modules with each other to generate visual output. Every module has properties that can be updated in real time to change how the module is drawn. Each of these properties can also be updated automatically using audio features based on audio input (e.g. a microphone). 

## Who is behind modV?

* [Sam Wray](https://2xaa.fm), founder and core contributor
  * Created modV in 2014
* [Tim Pietrusky](https://nerddis.co), core contributor
  * Joined modV in 2016 when both Sam and Tim did a collab-talk + performance for dotJS 2016 in Paris

Since then we are using modV for shows around the world and also as part of [LiveJS](https://livejs.network). 

## Where is modV being used?

* [Various videos & images of modV in action](https://photos.google.com/share/AF1QipPhz8zKecemmMxnMbFdhFo-__tSmrmqMQzrD4YkE1MUFq-FQxqgePMFure5h05SfA?key=eDRWNVhnVjE4SXU5N0xSNVpBQkw0SmRxQ0JCZjFn)
* [Performance of 2xAA & NERDDISCO for OPEN UP SUMMIT 2020 using modV 3.x](https://www.youtube.com/watch?v=RhM3arvVAPM)
* [A talk about modV 2.x at dotJS 2016](https://www.youtube.com/watch?v=GA7-OfYSzvA)


---

# 2. How to use modV

Before we dive into creating audio reactive visuals, we want to explain how the UI of modV is structured so we can talk about the same things. 

## User Interface

![modV User Interface Overview](media/20201204_modV_UI_overview.png)

* **A**: `Groups`. A Group contains modules. Modules within Groups can be arranged to change the drawing order. 
* **B**: `Module`. A module represents a visual element that draws something to the screen, like the `Text` module that can draw any kind of user generated text to the `Main Output`
* **C**: `Info View`. Shows information about the different panels in modV when you hover over them using your mouse. 
* **D**: `Gallery`. Contains all modules that can be added to a `Group`. Modules are categorized as `2D` (Canvas 2D), `ISF` (Interactive Shader Format) and `Shader` (WebGL Shader / GLSL). You can either scroll the list or use the search box at the top to find a specific module. 
* **E**: `Input config`.  The panel allows creation of Input Links. Select a `Module Control` in the `Module Inspector`, then use the `Input Config` to assign an `Audio Feature`, `MIDI control` or `Tween` to automate the Module Control.
* **F**: `Property Inspector`
* **G**: `Preview`. Shows the output of all enabled groups and modules. When you leave the default `Main Output`, it will show exaclty the same output as the `Output Window`
* **H**: `Input Device Config`

### Output Window

This window will be used to render the output of all enabled groups and modules. It can be used on a second screen or projector to show the generated visuals full screen. 

![modV output window](media/20201204_modv_output_window.png)


## Menu

There are multiple menu elements, but the most important ones are `File` and `View`:

* `File`
  * `Open Preset`: Load a preset from your computer into modV
  * `Save Preset`: Save a preset as JSON onto your computer that contains all groups, modules & settings of what you have created in modV
  * `Open Media Folder`; Uses your default application for exploring files (e.g. Finder on MacOS or Explorer on Windows) to open the `media` folder of modV. There you can put your images or third-party modules.
* `View`
  * `New Output Window`: Opens the output window if this was closed. 
  * `Reload`; As modV is a web application we can reload modV as we can reload any website in order to get a clean state.
  * `Reset Layout`: Makes sure that all panels are back in their original position after they were rearranged.  



## Rearrange the panel layout

Every panel in modV can be dragged and placed into different positions. This makes it possible to create any layout you want and feel comfortable with. 

* Move the `Info view` by clicking on it's title (keep the mouse button pressed) and dragging it onto the title of the `Gallery`. When you release the mouse button again, you can drop the `Info View` as a second tab besides the `Gallery`
* To give the `Gallery` even more space, we can click (keep the mouse button pressed) on the border between the `Gallery` and the `Input config`. Then we can move the mouse to the right to make the `Gallery` wider



## Add a module to a group

Now let's get some visual output. 

### Text

* Find the `Text` module in the `Gallery`, either by enterting the modules name into the search box or by scrolling trough the list of modules (hint: It's a `2D` module)
* Double click the module to add it to the active group (you might have to click on the group to focus it) or drag and drop it into the group
* Enable the module by clicking into the `Enable` checkbox, which will render the module into the `output`
* We still don't see anything, so let's go into the `Property Inspector` and change the property `text` by putting any text inside
* We still don't see very much, as a black text is rendered on a dark background, so let's change the `fillColor` to red
* To make the text bigger, we increase the `size`
* Now let's also enable `stroke` which gives us a white border around each character
* We also can change the `font`, where we can select any font that is installed on the computer
* Why do we see a morph effect and not just the 2D text? 

## Change group preferences

Each group determines how it's modules are drawn to the `Output window`. The default for this is `inherit`, which means that each frame is drawn on top of the frame before and the output is not cleared. Let's change this.

* Open the `group preference` by clicking on the arrow on the left side of the group, which will show you these options
  * **Enable**: Draws the group to the `Output window` if enabled
  * **Inherit**: If enabled, the Group will draw the output of the preceeding Group to the screen before drawing contained Modules. If there is only one Group enabled, the Group will draw the output of itself to the screen before drawing contained Modules
  * **Clearing**: If enabled, the Group will clear its Canvas before drawing contained Modules. If there is only one Group enabled and Inherit is on, clearing will have no affect.
  * **Pipeline**: If enabled, the Group will not draw each Module's output directly to the Group's Canvas. After each Module has drawn, the Group will cache the output, clear the Group Canvas and the next Module will recieve the cached result as its input. [Find more info & example](https://modv.vcync.gl/v3/guide/groups.html#pipeline)
  * **Alpha**: Sets the opacity of the group. 
  * **Blend**: Sets the Blend (e.g. Multiply) or Composition mode (e.g. Source in) of the group.
* We disable `Inherit` and enable `Clearing` and close the `group preference` by clicking on the arrow again
* Now our `Text` is rendered only once


## Use a filter: Pixelate

There are two kind of modules in modV. Some of them draw something "new", like the `Text` module did. Others changing the output based on what is actually drawn and do something with this. We call them `filter`, even when they are also "just" modules. 

* In the `Gallery` find the `Pixelate` 2D module
* Add it into the same group as the `Text`, but make sure to add it on the right side of `Text`

The order inside of a group matters, the modules are drawn on top of each other from left to right!

* We `Enable` the `Pixelate` module and see that the `Text` has a pixel-effect
* With focus on the `Pixelate` module we can now change the `Amount` in the `Property Inspector` to see the pixels getting bigger and smaller, depending in which direction the `Amount` is changed


## Change module preferences

Each module also has some default preferences which are visible all the time.

* Take a look the preferences:
  * **Enable**: If enabled, draws the module to the output.
  * **Alpha**: Sets the opacity of the module. 
  * **Blend**: Sets the Blend (e.g. Multiply) or Composition mode (e.g. Source in) of the module
* We set the `Blend` to `Multiply` and the `Alpha` to 0.5


## Add another group and rename it

We can also add more groups into modV.

* In `Groups`, click on the `New Group` button and see a new group under the first group
* Initially, the group is disabled, so let's enable it by clicking on the checkbox in the top left corner
* We also want to rename it, which can be done by double clicking on the group label `New Group` and entering a text
* Confirm this by pressing the Enter key

This makes it possible to group modules together that work well with each other. And during a live performance we can enable / disable a couple of modules at the same time. 


## Save / Load a preset

A preset contains all the groups and modules with all of their properties and can be used to switch between different configurations or exchange this with other people. In this workshop we use it to save the things we are doing or to load the predefined [presets](/presets) that come with the repository.

To make sure we don't loose what you already created, we first want to save our preset

* Im the menu choose `File` > `Save Preset`, which opens a dialog
* Enter a name for your preset and save it somewhere on your computer
* The file that was created is named `<name_of_preset>.json`

Now that we know how to save our configuration, we can load a preset

* Im the menu choose `File` > `Open Preset`, which opens a dialog
* There you can select the preset [001_modV_in_Action.json](presets/001_modV_in_Action.json) that comes with this repository
* After it is loaded, you will more or less see what we have created so far

---

# Breakout Session A

* Play around with modV and get used to the UI
* Add modules from the `Gallery` and change their properties in the `Property Inspector` to see how they behave and interact with other modules

---

## Add images into modV

Let's put an image (like a logo) into modV and apply some visual effects. 

* In the menu choose `View` > `Open Media Folder` which opens the media folder
  * If you want to know more about the strucutre of the media folder, please take a look at the [documentation](https://modv.vcync.gl/v3/guide/media.html#media-folder)
* Navigate into `default` > `image` and put any image (not SVG) into the folder
* In modV find the `Texture 2D` module in the `Gallery` and add it to the second group
* In the `Property Inspector` for the module select `image` in the `texture` property, which adds a new list underneath
* In the list, select the image you added into the `image` folder

Now that we have the image in modV, we can change some properties to make it fit the output window, depending on how small or big it is. 

---

## Add modules into modV

The biggest power over modV is that you can extend it with your own or other third-party modules as modV supports different kind of visual sources. So let's see how this can be done. 

### Module licence

* Taking stuff from other people
   * Shadertoy: [Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0)](https://creativecommons.org/licenses/by-nc-sa/3.0/deed.en_US)
   * CodePen: [MIT](https://opensource.org/licenses/MIT)
 * Make clear that taking resources from someone else should always be credited as we don’t want to steal anyone's work AND check the license

### Add ISF shader into modV

For this workshop we are collaborating with [ilithya](https://twitter.com/ilithya_net) and [eliza](https://twitter.com/iamelizasj) which means we can use their awesome "[Movement](https://codepen.io/ilithya/pen/poyMNvG)" shader to show how audio reactivity works. As the file should only be used in this workshop, we will provide the URL to shader when the actual workshop happens. For everyone else doing this tutorial, you can pick one of the [modules](/modules) in the repository. 

* In the menu choose `View` > `Open Media Folder` which opens the media folder
* Navigate into `default` > `module` and put the `ilithya_eliza_movement.js` into it (or any other custom module)
  * Custom modules must follow the implemention guide lines of modV in order to work, see TODO add resources for Canvas2D + ISF

The original shader was published on CodePen and we used the ISF editor to parameterize it. All parameters that you add in ISF are also automatically available in modV. 


### Use blending to make logo visible

The last thing we want to do is to make the logo visible again. 

* Select the `Texture 2D` module and select `Source in` in the `Module preferences` as a `Blend`


[002_custom_image_and_custom_module.json](presets/002_custom_image_and_custom_module.json)

---

# 3. Use Audio in modV

## Audio processing in modV

* How does audio processcing work in modV by using Meyda
* [Demo of Audio Features](https://jsbin.com/movezix/15/edit?js,output)
* Differences of Meyda to FFT

Demo: Custom image + Audio reactive scale using the microphone

## Audio routing

* Get external audio into modV

Demo: Audio routing with external audio source

## Audio reactive visuals

* Use Audio reactive parameters, smoothing
* Load custom image like a logo to recreate the modV workshop teaser

---

# Breakout Session B

* Play around with audio reactivity
* Use RMS / ZCR / Energy and find out how they are different from each other
* Use an expression to boost a value
* Combine different audio features with each other

---

# 4. Techniques to get certain effects

>Note: Concentrics has been used an example module here. This can be substituted for any module drawing to the screen. Feel free to experiment!

## Tunnel effect

|Before|After|
|---|---|
|![Tunnel effect before](/media/004/modV_PW8rYj8YFc.png)|![Tunnel effect after](/media/004/modV_73QoFUWI5L.png)|

#### Module order

1. scale
    * Update prop `scale` to anything over or under 0
2. Concentrics

## Background fade

|Before|After|
|---|---|
|![Background fade before](/media/004/modV_PW8rYj8YFc.png)|![Background fade after](/media/004/modV_3yssjuhJxg.png)|

#### Module order

1. block-color
    * Update prop `Alpha` to a low value over 0
2. Concentrics

## Hue rotation for trailing colors

This builds upon the two previous techniques.

|Before|After|
|---|---|
|![Hue rotation before](/media/004/modV_QxgzbdBmoe.png)|![Hue rotation after](/media/004/modV_UN7Y5aUxhL.png)|

#### Module order

1. Hue-Saturation
    * Update prop `Hue` to anything just over or under 0
2. block-color
    * Update prop `Alpha` to a low value over 0
3. scale
    * Update prop `scale` to anything over or under 0
4. Concentrics

## Text mask

This builds upon the three previous techniques.

|Before|After|
|---|---|
|![Text mask before](/media/004/modV_UN7Y5aUxhL.png)|![Text mask after](/media/004/modV_slxL53Vi1d.png)|

### Group 1

#### Group settings
* Un-check `Inherit` in the group settings
    * This prevents the group from drawing the output of the previous (or last, if it's the first group) to the screen again

#### Module order
1. Hue-Saturation
    * Update prop `Hue` to anything just over or under 0
2. block-color
    * Update prop `Alpha` to a low value over 0
3. scale
    * Update prop `scale` to anything over or under 0
4. Concentrics

### Group 2

#### Module order
1. Text
    * Ensure the text fill color is `#000000` (black)
    * Set the blend mode of the Module to `Difference`

## Liquid text

Load /presets/004_Liquid_Text.json.

This preset makes heavy use of Blend modes and two very powerful ISF shaders, "Edge Distort" and "Optical Flow Distort".
Combined in the right way it's possible to achieve a "liquid" effect.

This is similar to the effect seen in a video clip of modV from JSConf EU 2018, though that was modV 2 so the composition was likely different.

---

# Breakout Session C

* Play around with the advanced techniques, load each preset once and see how they behave

---

# 5. Use modV for a live performance

   * How to recover from a crash
   * How to use MIDI
   * Record the Video with OBS
     * Second screen for the output window
   * Streaming with OBS
   * Collaborate over Remote Desktop 
     * If we can make this work :D

---

2020 by [Sam Wray](https://2xaa.fm) & [Tim Pietrusky](https://nerddis.co)