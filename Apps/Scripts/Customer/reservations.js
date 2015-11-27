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
                React.createElement('a', { 'href': '#', className: 'reservationsBox', 'onClick': this.onClick },
                    React.createElement('div', { className: 'panel panel-default' },
                        React.createElement('div', { className: 'panel-body' },
                            React.createElement('h2', null,
                                resources.ReservationsHeader))))
            );
        }
    });

    var DateSelector = React.createClass({
        getInitialState: function () {
            return { dates: [] };
        },
        componentDidMount: function () {
            var url = 'api/reservation/dates/5';
            $.ajax({
                url: url,
                dataType: 'json',
                cache: false,
                success: function (data) {
                    this.setState({ dates: data });
                }.bind(this),
                error: function (xhr, status, err) {
                    console.error(url, status, err.toString());
                }.bind(this)
            });
        },
        render: function () {
            var specificDates = this.state.dates.map(function (date) {
                return React.createElement(SpecificDate, { 'key': date.Name, 'date': date });
            });
            return (
                React.createElement('div', null,
                    specificDates)
            );
        }
    });

    var SpecificDate = React.createClass({
        onClick: function () {

        },
        render: function () {
            return (
                React.createElement('button', { className: 'specificDate', 'onClick': this.onClick },
                    this.props.date.Name)
            );
        }
    });

    var TimeSelector = React.createClass({
        render: function () {
            var specificDates = this.state.dates.map(function (date) {
                return React.createElement(SpecificDate, { 'key': date.Name, 'date': date });
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
