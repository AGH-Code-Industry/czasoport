# Generating Documentation Manually

Project uses `DocFX` to generate documentation.

GitHub will generate documentation automatically on each push to `master` branch. However, before merging branch you are currently working to `master`, it is recommended to generate documentation locally and check if everything is fine.

> [!NOTE]
> Updating the documentation after making relevant changes is required by anyone working on the project.

## Installing DocFX

Please follow steps described in section `Create New Website` in [DocFX installation guide](https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html#2-use-docfx-as-a-command-line-tool).

## Czasoport documentation structure

Two important folders in `Documentation` folder are: `manual` and `resources`.

`Manual` folder contains Markdown files, that are basically standalone articles published on the site. In order to add new article, create new .md file in one of the subfolders and add link to it in `manual/toc.yml` file. Structure of toc.yml file is self-explanatory.

`Resources` folder contains images and other files that are used in articles. Folder structure should follow structure of `manual` folder, but resources for each article should be placed in additional folder with the name of the article.

Please check how this picture of a cute cat is placed in the folders in order to understand how it works:

`![Cute cat](~/resources/tutorials-info/generating-docs/cat.jpg)`

![Cute cat](~/resources/tutorials-info/generating-docs/cat.jpg)