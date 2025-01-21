// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import { Calendar } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';

// Import FullCalendar CSS 
import '@fullcalendar/daygrid/main.css';



document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new Calendar(calendarEl, {
        plugins: [dayGridPlugin], // Load the day grid view
        initialView: 'dayGridMonth', // Default to month view
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay', 
        },
        events: '/HabitTrackingGoal/GetHabitsForCalendar', // Fetch habit events from the backend
        eventClick: function (info) {
            alert('Habit: ' + info.event.title + '\nDate: ' + info.event.start.toDateString());
        },
        dateClick: function (info) {
            alert('Clicked on: ' + info.dateStr);
        },
    });

    calendar.render();
});


