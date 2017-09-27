BuienRadar API - Retreiving rain prediction and weather information for the Netherlands
====================

[![License](https://img.shields.io/badge/license-MIT%20License-blue.svg)](http://doge.mit-license.org)

*A .NET API for fetching rain predictions and weather information from BuienRadar, including demonstration application.*

BuienRadar API is an API that fetches raw rain precipitation through the free Buienradar API at "http://gadgets.buienradar.nl/data/raintext?lat=LATTITUDE&lon=LONGITUDE" and weatherstation information at "https://xml.buienradar.nl/". The data is cached for a settable duration in order to not overburden the buienradar servers. It also provides a rain info-to-text translation finding perionds of rain and describing these.

To demonstrate the usage of the API, a minimal application has been included that fetches the data, based on any given location (using the Google location service)

## Features

* Fetching of precipitation data and weatherstation information
* Data caching 
* Synchronous and asynchronous fetching
* Smart(ish) data-to-text parsing
* Demonstation Application
* MIT License

## Tested  

* C# .NET
* MONO, .NET core are to be tested. 

## Downloading

This Application is not available through Nuget can only be downloaded from GitHub. 

- By directly loading fetching the Archive from GitHub: 
  1. Go to [https://github.com/thijse/DSPhotoSorter](https://github.com/thijse/DSPhotoSorter)
  2. Click the DOWNLOAD ZIP button in the panel on the
  3. Optionally rename the uncompressed folder **DSPhotoSorter-master** to **DSPhotoSorter**.

- By downloading a release
  1. Go to releases
  2. Click the 'Source code' button


## Copyright

BuienRadar API is provided Copyright © 2017 under MIT License.

