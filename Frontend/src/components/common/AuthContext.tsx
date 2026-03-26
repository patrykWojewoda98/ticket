"use client";

import { createContext, useContext, useState, useEffect } from "react";

type AuthContextType = {
  isAuthenticated: boolean;
  user: any | null;
  isLoaded: boolean; // NOWOŚĆ: flaga informująca, czy sprawdzanie sesji dobiegło końca
  setIsAuthenticated: (value: boolean) => void;
  setUser: (user: any) => void; // Dodajemy, żeby móc ustawić usera po loginie
  logout: () => void;
};

const AuthContext = createContext<AuthContextType | null>(null);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState<any | null>(null);
  const [isLoaded, setIsLoaded] = useState(false); // Na początku false

  useEffect(() => {
    const savedUser = localStorage.getItem("user");
    if (savedUser) {
      try {
        const parsedUser = JSON.parse(savedUser);
        setUser(parsedUser);
        setIsAuthenticated(true);
      } catch (e) {
        console.error("Błąd parsowania użytkownika", e);
        localStorage.removeItem("user");
      }
    }
    setIsLoaded(true); // KONIEC SPRAWDZANIA - teraz komponenty mogą działać
  }, []);

  const logout = () => {
    localStorage.removeItem("user");
    setIsAuthenticated(false);
    setUser(null);
    window.location.href = "/customer/login";
  };

  return <AuthContext.Provider value={{ isAuthenticated, user, isLoaded, setIsAuthenticated, setUser, logout }}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be used within AuthProvider");
  return context;
}
