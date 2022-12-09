## Readme for ST2ITS2 repository

This repository will contain all material for the course.

### Slider server

In `slide_server` the revealjs framework is cloned. If you wan't to run the server locally. Go into the directory, install the needed NPM packages and start the server.

You may need to install node. This can be found here [NodeJS Download](https://nodejs.org/en/download/), from here download and install the latets LTS version for your operating system.

To start the server now (from an terminal/cmd/powershell)

```shell
cd slide_server
npm install
npm start
```

Now the server is running (unless there are errors in the terminal) and you can in you browser go to [http://localhost:8010/](http://localhost:8010/) to se the slides.

*Note*: `npm install` should only be run when NPM packages are changed.

#### To show slides notes

Each slide set can be found in ./slide_server/slides/Week xx/ and contains an index.html file. In this find there is a javascript property

`"showNotes": false,`

To show notes, flip this boolean to true

#### To show PDFs

When the slide server is running, you can convert these to PDFs by appending `?print-pdf` to the url, e.g.

http://localhost:8010/slides/Week 01/Introduction/?print-pdf and then using the systems/browser print menu to print to a PDF file.