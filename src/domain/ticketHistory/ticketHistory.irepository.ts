import { TicketHistory } from "@/domain/ticketHistory/ticketHistory.entity";

export interface ITicketHistoryRepository {
  findByTicketId(ticketId: string): Promise<TicketHistory[]>;
}
