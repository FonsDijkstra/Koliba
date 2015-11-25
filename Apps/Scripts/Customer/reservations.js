$.get('api/reservation/resources', function (resources, status) {

    var ReservationsBox = React.createClass({
        render: function () {
            return (
                React.createElement('a', { 'href': '#', styleName: 'display:block' },
                    React.createElement('div', { className: 'panel panel-default' },
                        React.createElement('div', { className: 'panel-body' },
                            React.createElement('h2', null,
                                resources.ReservationsHeader))))
            );
        }
    });

    ReactDOM.render(
        React.createElement(ReservationsBox, null),
        document.getElementById('reservations-content')
    );
});
