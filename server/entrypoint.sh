#!/bin/bash
dotnet ef database update
exec dotnet Bookify.dll