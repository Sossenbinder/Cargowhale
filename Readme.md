# Cargowhale

(Under construction)

Cargowhale is a tool to automate backing up a database into a docker container. Imagine your developers want to have fresh data by the day in their local environment, but you don't want to give them access. With Cargowhale, you can simply have daily (or self-scheduled CRON backed) intervals of backup, and Cargowhale will produce a finished docker image your developers can simply pull to receive their own copy of fresh data, without modifying the source database. Data isolation, yeah!

## Getting Started

### Dependencies

* No direct dependencies. Cargowhale is available as image over at docker.io/sossenbinder/cargowhale, or can be run as command line tool.
* Requires a sql database and a blob storage to back up (Sadly azurite does not work as of now)

### Executing program

* You can check the /help output to see a list of required configs. Configs can either be passed as cli arguments, or in form of a config file.
* For a sample, see /sample/sampleConfig.json

## Authors

* [Sossenbinder](https://github.com/Sossenbinder)

## Version History

* 0.1
    * Initial Release