import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DateService {
  constructor() {}

  toDateTime(time: string, date: Date) {
    const split = time.split(':', 2);
    let hour = parseInt(split[0], 10);
    const rest = split[1].split(' ', 2);
    const min = parseInt(rest[0], 10);
    const ampm = rest[1];
    if (ampm === 'PM') {
      if (hour === 12) {
        hour -= 12;
      } else {
        hour += 12;
      }
    }
    let newDate = new Date(date);
    newDate.setHours(hour, min);
    return newDate;
  }

  toTimeString(date: Date) {
    let hour = new Date(date).getHours();
    const min = new Date(date).getMinutes();
    let ampm = ' AM';
    if (hour > 12) {
      hour -= 12;
      ampm = ' PM';
    }
    if (hour === 0) {
      hour = 12;
      ampm = ' PM';
    }
    return hour + ':' + min + ampm;
  }
}
