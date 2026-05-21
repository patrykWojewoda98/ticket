"use client";

import { createContext, useContext, useState, useEffect } from "react";
<<<<<<< HEAD
import { secureStorage, sanitizeInput } from "@/lib/security";
=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e

type AuthContextType = {
  isAuthenticated: boolean;
  user: any | null;
<<<<<<< HEAD
  isLoaded: boolean;
  setIsAuthenticated: (value: boolean) => void;
  setUser: (user: any) => void;
  logout: () => void;
  login: (userData: any) => void;
=======
  isLoaded: boolean; 
  setIsAuthenticated: (value: boolean) => void;
  setUser: (user: any) => void;
  logout: () => void;
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
};

const AuthContext = createContext<AuthContextType | null>(null);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState<any | null>(null);
<<<<<<< HEAD
  const [isLoaded, setIsLoaded] = useState(false);

  useEffect(() => {
    // Load user data securely on app start
    const loadUser = () => {
      try {
        const savedUser = secureStorage.getItem("user");
        if (savedUser) {
          const parsedUser = JSON.parse(savedUser);
          // Validate user data structure
          if (parsedUser && typeof parsedUser === 'object' && parsedUser.id && parsedUser.email) {
            setUser(parsedUser);
            setIsAuthenticated(true);
          } else {
            // Invalid data, clear it
            secureStorage.removeItem("user");
          }
        }
      } catch (e) {
        console.error("Error loading user data:", e);
        secureStorage.removeItem("user");
      } finally {
        setIsLoaded(true);
      }
    };

    loadUser();

    // Auto-logout after 10 hours of inactivity
    const inactivityTimeout = 10 * 60 * 60 * 1000; // 10 hours
    let logoutTimer: NodeJS.Timeout;

    const resetTimer = () => {
      clearTimeout(logoutTimer);
      logoutTimer = setTimeout(() => {
        logout();
      }, inactivityTimeout);
    };

    const events = ['mousedown', 'mousemove', 'keypress', 'scroll', 'touchstart'];
    events.forEach(event => {
      document.addEventListener(event, resetTimer, true);
    });

    resetTimer();

    return () => {
      clearTimeout(logoutTimer);
      events.forEach(event => {
        document.removeEventListener(event, resetTimer, true);
      });
    };
  }, []);

  const login = (userData: any) => {
    if (!userData || !userData.id || !userData.email) {
      console.error("Invalid user data provided to login");
      return;
    }

    // Sanitize user data before storing
    const sanitizedUser = {
      ...userData,
      name: sanitizeInput(userData.name || '', 100),
      email: sanitizeInput(userData.email, 254),
    };

    try {
      secureStorage.setItem("user", JSON.stringify(sanitizedUser));
      setUser(sanitizedUser);
      setIsAuthenticated(true);
    } catch (error) {
      console.error("Failed to save user data:", error);
    }
  };

  const logout = () => {
    try {
      secureStorage.clear(); // Clear all secure storage
      setIsAuthenticated(false);
      setUser(null);
      // Clear any cookies
      document.cookie.split(";").forEach(c => {
        document.cookie = c.replace(/^ +/, "").replace(/=.*/, "=;expires=" + new Date().toUTCString() + ";path=/");
      });
      window.location.href = "/customer/login";
    } catch (error) {
      console.error("Error during logout:", error);
    }
  };

  return (
    <AuthContext.Provider
      value={{
        isAuthenticated,
        user,
        isLoaded,
        setIsAuthenticated,
        setUser,
        logout,
        login
      }}
    >
      {children}
    </AuthContext.Provider>
  );
=======
  const [isLoaded, setIsLoaded] = useState(false); 

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
    setIsLoaded(true); 
  }, []);

  const logout = () => {
    localStorage.removeItem("user");
    setIsAuthenticated(false);
    setUser(null);
    window.location.href = "/customer/login";
  };

  return <AuthContext.Provider value={{ isAuthenticated, user, isLoaded, setIsAuthenticated, setUser, logout }}>{children}</AuthContext.Provider>;
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be used within AuthProvider");
  return context;
}
