import { TicketStatus } from "@/domain/ticketStatus/ticketStatus.entity";

export interface ITicketStatusRepository {
  findByName(name: string): Promise<TicketStatus | null>;
  findAll(): Promise<TicketStatus[]>;
  create(data: Omit<TicketStatus, "id">): Promise<TicketStatus>;
  update(id: string, data: Partial<Omit<TicketStatus, "id">>): Promise<TicketStatus>;
  delete(id: string): Promise<void>;
}
