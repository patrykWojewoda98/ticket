import { z } from "zod";

export const TicketCategorySchema = z.object({
  name: z.string().min(2, "Nazwa kategorii musi mieć co najmniej 2 znaki").max(50, "Nazwa kategorii może mieć maksymalnie 50 znaków").trim(),
});

export type TicketCategoryInput = z.infer<typeof TicketCategorySchema>;
