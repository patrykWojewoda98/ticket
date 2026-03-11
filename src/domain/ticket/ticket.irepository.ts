import { Ticket } from "@/domain/ticket/ticket.entity";

export interface ITicketRepository {
  findByUserId(userId: string): Promise<Ticket[]>;
  findByAssigneeId(assigneeId: string): Promise<Ticket[]>;
  findAll(): Promise<Ticket[]>;
  create(data: Omit<Ticket, "id" | "createdAt" | "updatedAt">): Promise<Ticket>;
  update(id: string, data: Partial<Omit<Ticket, "id" | "createdAt" | "updatedAt">>): Promise<Ticket>;
  delete(id: string): Promise<void>;
}
