"use client";

import { Button } from "@/components/ui/button";
import { Facebook, Twitter, Instagram } from "lucide-react";

export default function Footer() {
  const socialLinks = [
    { icon: <Facebook className="w-5 h-5" />, label: "Facebook" },
    { icon: <Twitter className="w-5 h-5" />, label: "Twitter" },
    { icon: <Instagram className="w-5 h-5" />, label: "Instagram" },
  ];

  return (
    <footer className="bg-white border-t">
      <div className="flex md:flex-row flex-col justify-between items-center gap-4 mx-auto px-6 py-6 container">
        <div className="text-slate-500 text-sm md:text-left text-center">&copy; {new Date().getFullYear()} Wszelkie prawa zastrzeżone.</div>

        <div className="hidden md:block border-slate-200 border-l h-6"></div>

        <div className="flex md:flex-row flex-col items-center gap-4">
          {socialLinks.map((item) => (
            <Button key={item.label} variant="ghost" className="hover:bg-slate-100 p-2 rounded-full transition-colors" aria-label={item.label}>
              {item.icon}
            </Button>
          ))}
        </div>
      </div>
    </footer>
  );
}
