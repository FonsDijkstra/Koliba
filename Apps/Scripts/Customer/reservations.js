$.get('api/reservation/resources', function (resources, status) {

    var ReservationsBox = React.createClass({
        onClick: function () {
            history.pushState(new reservationState(1, null, null), 'Resrv')
            dateSelector();
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
            history.pushState(new reservationState(2, this.props.date), 'Dates')
            timeSelector(this.props.date);
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

    function reservationState(step, date, time) {
        this.step = step;
        this.date = date;
        this.time = time;
    }

    window.onpopstate = function (event) {
        console.log(event.state);
        if (event.state != null) {
            if (event.state.step === 1) {
                dateSelector();
            }
            if (event.state.step === 2) {
                timeSelector(event.state.date);
            }
            if (event.state.step === 3) {
                    
            }
        } else {
            reservationsBox();
        }
    };

    reservationsBox();
});
