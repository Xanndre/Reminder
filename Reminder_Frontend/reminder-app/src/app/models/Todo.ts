import { Notification } from 'src/app/models/Notification';

export interface Todo{
    id: number;
    title: string;
    description: string;
    date: Date;
    notifications: Notification[];
}