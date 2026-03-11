export interface Comment {
  id: string;
  userId: string;
  ticketId: string;
  content: string;
  createdAt: Date;
  updatedAt: Date;
}
