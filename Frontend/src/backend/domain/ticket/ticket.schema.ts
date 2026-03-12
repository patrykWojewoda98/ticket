import { z } from "zod";

export const TicketSchema = z.object({
  title: z.string().min(3, "Tytuł musi mieć co najmniej 3 znaki").max(100, "Tytuł może mieć maksymalnie 100 znaków").trim(),
  description: z.string().min(10, "Opis musi mieć co najmniej 10 znaków").max(1000, "Opis może mieć maksymalnie 1000 znaków").trim(),
});

export type TicketInput = z.infer<typeof TicketSchema>;
