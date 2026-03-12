import { TicketHistory } from "@/backend/domain/ticketHistory/ticketHistory.entity";

export interface ITicketHistoryRepository {
  findByTicketId(ticketId: string): Promise<TicketHistory[]>;
}
