import { z } from "zod";

export const TicketAttachmentSchema = z.object({
  filename: z.string().min(1, "Nazwa pliku nie może być pusta").max(255, "Nazwa pliku może mieć maksymalnie 255 znaków").trim(),
  path: z.string().min(1, "Ścieżka pliku nie może być pusta").max(1000, "Ścieżka pliku może mieć maksymalnie 1000 znaków").trim(),
});

export type TicketAttachmentInput = z.infer<typeof TicketAttachmentSchema>;
