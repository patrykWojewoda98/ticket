export interface TicketAttachment {
  id: string;
  ticketId: string;
  filename: string;
  path: string;
  uploadedBy?: string | null;
  createdAt: Date;
}
