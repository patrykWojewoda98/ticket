import { TicketAttachment } from "@/domain/ticketAttachment/ticketAttachment.entity";

export interface ITicketAttachmentRepository {
  findByTicketId(ticketId: string): Promise<TicketAttachment[]>;
  create(data: Omit<TicketAttachment, "id" | "createdAt">): Promise<TicketAttachment>;
  update(id: string, data: Partial<Omit<TicketAttachment, "id" | "createdAt">>): Promise<TicketAttachment>;
  delete(id: string): Promise<void>;
}
