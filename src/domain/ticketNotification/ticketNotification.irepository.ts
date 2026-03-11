import { TicketNotification } from "@/domain/ticketNotification/ticketNotification.entity";

export interface ITicketNotificationRepository {
  findByTicketId(ticketId: string): Promise<TicketNotification[]>;
  create(data: Omit<TicketNotification, "id" | "createdAt">): Promise<TicketNotification>;
  update(id: string, data: Partial<Omit<TicketNotification, "id" | "createdAt">>): Promise<TicketNotification>;
  delete(id: string): Promise<void>;
}
