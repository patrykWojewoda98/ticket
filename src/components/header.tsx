import { Rocket, Plus } from "lucide-react";
import { Button } from "@/components/ui/button";

export default function Header() {
  return (
    <header className="bg-white px-6 py-4 border-slate-200 border-b w-full">
      <div className="flex justify-between items-center mx-auto max-w-7xl">
        <div className="group flex items-center gap-3 cursor-pointer">
          <div className="bg-slate-100 group-hover:bg-slate-200 p-2 border border-slate-200 rounded-full transition-colors">
            <Rocket className="fill-slate-700 w-5 h-5 text-slate-700" />
          </div>
          <span className="font-bold text-slate-900 text-xl tracking-tight">Ticket</span>
        </div>

        <nav className="hidden md:flex items-center gap-8">
          {["Strona główna", "Rankingi", "Cennik", "Kontakt"].map((item) => (
            <a key={item} href="#" className="font-medium text-slate-500 hover:text-slate-900 text-sm transition-colors">
              {item}
            </a>
          ))}
        </nav>

        <div className="flex items-center gap-6">
          <a href="#" className="font-medium text-slate-500 hover:text-slate-900 text-sm transition-colors">
            Zaloguj się
          </a>

          <Button className="bg-slate-900 hover:bg-slate-800 shadow-sm px-5 py-5 rounded-full text-white">
            <Plus className="mr-2 w-4 h-4" />
            Dodaj produkt
          </Button>
        </div>
      </div>
    </header>
  );
}
