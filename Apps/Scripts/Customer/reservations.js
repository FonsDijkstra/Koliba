$.get('api/reservation/resources', function (resources, status) {

    var ReservationsBox = React.createClass({
        onClick: function () {
            ReactDOM.render(
                React.createElement(DateSelector, null),
                document.getElementById('reservations-content')
            );
        },
        render: function () {
            return (
                React.createElement('a', { 'href': '#', styleName: 'display:block', 'onClick': this.onClick },
                    React.createElement('div', { className: 'panel panel-default' },
                        React.createElement('div', { className: 'panel-body' },
                            React.createElement('h2', null,
                                resources.ReservationsHeader))))
            );
        }
    });

    var SpecificDate = React.createClass({
        render: function () {
            return (
                React.createElement('button', null, this.props.date)
            );
        }
    });

    var DateSelector = React.createClass({
        getInitialState: function () {
            return { dates: ['test1', 'test2'] };
        },
        render: function () {
            var specificDates = this.state.dates.map(function (date) {
                return React.createElement(SpecificDate, { 'date': date }, null);
            });
            return (
                React.createElement('div', null,
                    specificDates)
            );
        }
    });

    ReactDOM.render(
        React.createElement(ReservationsBox, null),
        document.getElementById('reservations-content')
    );
});
