$.get('api/reservation/resources', function (resources, status) {

    var ReservationsBox = React.createClass({
        onClick: function () {
            pushState(dateSelector, null, 'rsv-dates');
        },
        render: function () {
            return (
                React.createElement('div', { className: 'reservationsBox', 'onClick': this.onClick },
                    React.createElement('h2', null,
                        resources.ReservationsHeader))
            );
        }
    });

    var DateSelector = React.createClass({
        getInitialState: function () {
            return { dates: [] };
        },
        componentDidMount: function () {
            $.ajax({
                url: '/api/reservation/dates/5',
                dataType: 'json',
                cache: false,
                success: function (data) {
                    this.setState({ dates: data });
                }.bind(this)
            });
        },
        render: function () {
            var specificDates = this.state.dates.map(function (date) {
                return React.createElement(SpecificDate, { 'key': date.Name, 'date': date });
            }, this);
            return (
                React.createElement('div', null,
                    specificDates)
            );
        }
    });

    var SpecificDate = React.createClass({
        onClick: function () {
            pushState(timeSelector, [this.props.date], 'rsv-times');
        },
        render: function () {
            return (
                React.createElement('button', { className: 'specificDate', 'onClick': this.onClick },
                    this.props.date.Name)
            );
        }
    });

    var TimeSelector = React.createClass({
        getInitialState: function () {
            return { times: [] };
        },
        componentDidMount: function () {
            $.ajax({
                url: '/api/reservation/times',
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify(this.props.date),
                contentType: "application/json; charset=utf-8",
                processData: false,
                success: function (data) {
                    this.setState({ times: data });
                }.bind(this)
            });
        },
        render: function () {
            var specificTimes = this.state.times.map(function (time) {
                return React.createElement(SpecificTime, { 'key': time.Name, 'time': time, 'date': this.props.date });
            }, this);
            return (
                React.createElement('div', null,
                    specificTimes)
            );
        }
    });

    var SpecificTime = React.createClass({
        onClick: function () {
            //history.pushState(new reservationState(3, this.props.date, this.props.time), 'Times');
            partySelector(this.props.date, this.props.time);
        },
        render: function () {
            return (
                React.createElement('button', { className: 'specificTime', 'onClick': this.onClick },
                    this.props.time.Name)
            );
        }
    });

    function reservationsBox() {
        ReactDOM.render(
            React.createElement(ReservationsBox, null),
            document.getElementById('reservations-content')
        );
    }

    function dateSelector() {
        ReactDOM.render(
            React.createElement(DateSelector, null),
            document.getElementById('reservations-content')
        );
    }

    function timeSelector(date) {
        ReactDOM.render(
            React.createElement(TimeSelector, { 'date': date }),
            document.getElementById('reservations-content')
        );
    }

    function partySelector(date, time) {
        
    }

    function pushState(func, args, title) {
        setState(func, args);
        history.pushState(new appState(func.name, args), title);
    }

    function popState(state) {
        if (state == null) {
            setState(reservationsBox, null);
        } else {
            var func = null;
            if (state.name === dateSelector.name) {
                func = dateSelector;
            } else if (state.name == timeSelector.name) {
                func = timeSelector;
            } else {
                func = reservationsBox;
            }
            setState(func, state.args);
        }
    }

    function setState(func, args) {
        func.apply(null, args);
    }

    function appState(name, args) {
        this.name = name;
        this.args = args;
    }

    window.onpopstate = function (event) {
        popState(event.state);
    };

    reservationsBox();
});
