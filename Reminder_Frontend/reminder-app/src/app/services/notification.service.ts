import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Notification } from '../models/Notification';
import { NotificationCreate } from '../models/NotificationCreate';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private client: HttpClient) { }

  createNotification(notification: NotificationCreate){
    return this.client
      .post<Notification>('https://localhost:44340/api/Notifications', notification);
  }

  deleteNotification(id: number){
    return this.client.delete(`https://localhost:44340/api/Notifications/${id}`);
  }

  updateNotification(notification: Notification){
    return this.client.put<Notification>(`https://localhost:44340/api/Notifications/${notification.id}`, notification);
  }
}
