# Timeline

A simple CLI app to track and display past and future events.

## Usage

```
Description:
  Sample app for Tracking Events

Usage:
  timeline [command] [options]

Options:
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  add   Add an event
  del   Remove an event
  list  List all events
```


## Getting Started

1. Add an Event

```
$ timeline add --title "hello world 2" --date "2023-06-04"

1)Sun, 04 Jun 2023: hello world 2
```

2. List all events

```
$ timeline list

1)Sun, 04 Jun 2023: hello world 2
```


3. Delete an event

```
$ timeline del --id 1

event removed
```