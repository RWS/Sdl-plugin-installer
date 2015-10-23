# Sdl-plugin-installer

This is intended to be an universal installer for all SDL plugins. When this installed on the machine it will be installed as a default application for *.sdlplugin file type, meaning that everytime such a file is double clicked the installer will launch and guide through a series of steps that help you install the plugin you want. The installer has the following features:

1. Default application for *.sdlplugin file type.
2. Determine all installed versions of SDL Studio.
3. Allows installation only for plugin compatible SDL Studio versions.
3. Installs plugins for multiple installed versions of SDL Studio - based on user selection.
4. Remove previous installed versions of the plugin during installation.

OpenExchange was updated to allow publishing plugins without wrapping them up in a zip file. This eliminates the need of creating a dedicated installer for your plugin as long as the user have this universal installer on their machine. The universal installer is intended to be distributed together with SDL Studio 2015 but this is yet to be confirmed.

## Releases

You can get the latest versions from [here](http://www.translationzone.com/openexchange/app/sdlplugininstaller-797.html#85394).

##Contribution

You want to add a new functionality or you spot a bug please fill free to create a [pull request](http://www.codenewbie.org/blogs/how-to-make-a-pull-request) with your changes.

##Development Prerequisites

* [Studio 2014 or 2015](https://oos.sdl.com/asp/products/ssl/account/mydownloads.asp) - if you don't have a licence please use this [link](http://www.translationzone.com/openexchange/developer/index.html) and sign-up into SDL OpenExchange Developer Program
* [Studio 2014 SDK](http://www.translationzone.com/openexchange/developer/sdk.html)
* [Visual Studio 2013](http://www.visualstudio.com/downloads/download-visual-studio-vs) - express/community edition can be used
* [Inno Setup](http://www.jrsoftware.org/isinfo.php) - if you want to generate the installer

##Issues

If you find an issue you report it [here](https://github.com/sdl/SDL-Community/issues).
