"use client";

<<<<<<< HEAD
import { useState } from "react";
import { Loader2 } from "lucide-react";
=======
import Link from "next/link";
import { Loader2, Ticket } from "lucide-react";
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useLoginForm } from "@/hooks/useLoginForm";

export function LoginForm() {
<<<<<<< HEAD
  const { email, password, loading, errors, setEmail, setPassword, handleSubmit } = useLoginForm();
  const [showPassword, setShowPassword] = useState(false);
=======
  const { email, password, loading, setEmail, setPassword, handleSubmit } = useLoginForm();
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e

  return (
    <div className="slide-in-from-bottom-4 space-y-8 w-full max-w-[350px]">
      {/* Header*/}
      <div className="flex flex-col items-center space-y-3 text-center">
        <h1 className="font-black text-slate-900 text-3xl tracking-tighter">Witaj ponownie</h1>
        <p className="font-bold text-slate-400 text-sm">Wprowadź dane, aby przejść do panelu</p>
      </div>

<<<<<<< HEAD
      {/* General Error */}
      {errors.general && (
        <div className="bg-red-50 border border-red-200 rounded-lg p-3">
          <p className="text-red-700 text-sm font-medium">{errors.general}</p>
        </div>
      )}

=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
      {/* Formularz */}
      <form onSubmit={handleSubmit} className="space-y-5">
        <div className="space-y-2">
          <Label htmlFor="email" className="ml-1 font-black text-[10px] text-slate-400 uppercase tracking-widest">
            Adres Email
          </Label>
<<<<<<< HEAD
          <Input
            id="email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="nazwa@domena.pl"
            className={`bg-white border-slate-200 focus:border-transparent rounded-xl focus:ring-2 focus:ring-slate-900 h-12 font-medium placeholder:text-slate-300 transition-all ${
              errors.email ? 'border-red-300 focus:ring-red-500' : ''
            }`}
            required
            autoComplete="email"
          />
          {errors.email && (
            <p className="text-red-600 text-xs ml-1">{errors.email}</p>
          )}
=======
          <Input id="email" type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="nazwa@domena.pl" className="bg-white border-slate-200 focus:border-transparent rounded-xl focus:ring-2 focus:ring-slate-900 h-12 font-medium placeholder:text-slate-300 transition-all" required />
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
        </div>

        <div className="space-y-2">
          <div className="flex justify-between items-center ml-1">
            <Label htmlFor="password" className="font-black text-[10px] text-slate-400 uppercase tracking-widest">
              Hasło
            </Label>
<<<<<<< HEAD
            <button
              type="button"
              onClick={() => setShowPassword((value) => !value)}
              className="text-slate-500 text-[10px] uppercase tracking-widest hover:text-slate-900 transition"
            >
              {showPassword ? "Ukryj" : "Pokaż"}
            </button>
          </div>
          <Input
            id="password"
            type={showPassword ? "text" : "password"}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="••••••••"
            className={`bg-white border-slate-200 focus:border-transparent rounded-xl focus:ring-2 focus:ring-slate-900 h-12 font-medium placeholder:text-slate-300 transition-all ${
              errors.password ? 'border-red-300 focus:ring-red-500' : ''
            }`}
            required
            autoComplete="current-password"
          />
          {errors.password && (
            <p className="text-red-600 text-xs ml-1">{errors.password}</p>
          )}
        </div>

        <Button
          type="submit"
          disabled={loading}
          className="bg-slate-900 shadow-lg shadow-slate-100 rounded-full w-full h-12 font-bold text-white text-sm hover:scale-[0.98] active:scale-95 transition-all disabled:opacity-50 disabled:cursor-not-allowed"
        >
=======
          </div>
          <Input id="password" type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="••••••••" className="bg-white border-slate-200 focus:border-transparent rounded-xl focus:ring-2 focus:ring-slate-900 h-12 font-medium placeholder:text-slate-300 transition-all" required />
        </div>

        <Button type="submit" disabled={loading} className="bg-slate-900 shadow-lg shadow-slate-100 rounded-full w-full h-12 font-bold text-white text-sm hover:scale-[0.98] active:scale-95 transition-all">
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
          {loading ? <Loader2 className="w-5 h-5 animate-spin" /> : "Zaloguj się"}
        </Button>
      </form>
    </div>
  );
}
