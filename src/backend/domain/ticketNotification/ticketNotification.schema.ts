import { z } from "zod";

export const TicketNotificationSchema = z.object({
  message: z.string().min(1, "Wiadomość nie może być pusta").max(500, "Wiadomość może mieć maksymalnie 500 znaków").trim(),
  read: z.boolean().optional(),
});

export type TicketNotificationInput = z.infer<typeof TicketNotificationSchema>;
