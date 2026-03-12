import { TicketPriority } from "@/backend/domain/ticketPriority/ticketPriority.entity";

export interface ITicketPriorityRepository {
  findByName(name: string): Promise<TicketPriority | null>;
  findAll(): Promise<TicketPriority[]>;
  create(data: Omit<TicketPriority, "id">): Promise<TicketPriority>;
  update(id: string, data: Partial<Omit<TicketPriority, "id">>): Promise<TicketPriority>;
  delete(id: string): Promise<void>;
}
