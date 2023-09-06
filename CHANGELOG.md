# Changelog
All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

## [0.1.0] - 2023-07-07
### Initialize
- First release of this framework

## [0.1.1] - 2023-07-07
### Changes
- Update README.md

## [0.1.2] - 2023-07-08
### Added
- Add JamServices to allow user for easy register and unregister services

## [0.1.3] - 2023-07-08
### Changes
- Update README.md
- Introduce `JamInstaller` for services injection

## [0.1.4] - 2023-07-11
### Changes
- Replace `Log4Net` with self-implemented logger

## [0.1.5] - 2023-07-11
### Features
- Introduced a simple `AudioController` with Editor data creation helper

### Changes
- Rename `JamLogger` to `Logger`

## [0.1.6] - 2023-07-12
### Changes
- Rename log functions

### Improves
- `AudioController` robust update (null check and logs)

## [0.1.62] - 2023-09-06
### Fixes
- NPE fixes for GameProcedures / StateMachineRunner
- Should create procedure from Type.FullName