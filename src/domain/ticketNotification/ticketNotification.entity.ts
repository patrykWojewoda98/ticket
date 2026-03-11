export interface TicketNotification {
  id: string;
  ticketId: string;
  userId: string;
  message: string;
  read: boolean;
  createdAt: Date;
}
