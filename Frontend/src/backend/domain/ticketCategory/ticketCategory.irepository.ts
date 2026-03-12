import { TicketCategory } from "@/backend/domain/ticketCategory/ticketCategory.entity";

export interface ITicketCategoryRepository {
  findByName(name: string): Promise<TicketCategory | null>;
  findAll(): Promise<TicketCategory[]>;
  create(data: Omit<TicketCategory, "id">): Promise<TicketCategory>;
  update(id: string, data: Partial<Omit<TicketCategory, "id">>): Promise<TicketCategory>;
  delete(id: string): Promise<void>;
}
