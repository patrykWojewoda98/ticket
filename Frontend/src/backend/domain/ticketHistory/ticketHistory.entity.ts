export interface TicketHistory {
  id: string;
  ticketId: string;
  action: string;
  oldValue?: string | null;
  newValue?: string | null;
  userId?: string | null;
  createdAt: Date;
}
