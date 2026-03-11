import { z } from "zod";

export const UserSchema = z.object({
  email: z.email("Wprowadź poprawny format adresu e-mail.").min(3, "Adres e-mail musi mieć co najmniej 3 znaki.").max(255, "Adres e-mail może mieć maksymalnie 255 znaków.").toLowerCase().trim(),
  password: z.string().min(8, "Hasło musi mieć co najmniej 8 znaków.").max(50, "Hasło może mieć maksymalnie 50 znaków.").trim(),
});

export type UserInput = z.infer<typeof UserSchema>;
