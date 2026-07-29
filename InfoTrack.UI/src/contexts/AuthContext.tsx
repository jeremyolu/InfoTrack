// AuthContext.tsx
import { createContext, useContext, useState, useEffect } from 'react';
import { axiosClient, setUnauthorizedHandler } from '../clients/axiosClient';

import type { ReactNode } from 'react';
import type AuthContextType from '../types/data/AuthContextType';
import { HttpStatusCode } from 'axios';

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {

  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  const checkAuth = async () => {
    try {
      const response = await axiosClient.get('auth/account');

      if (response.status === HttpStatusCode.Ok) {
        setUser(response.data.result);
        setIsAuthenticated(true);
      } else {
        setIsAuthenticated(false);
      }
    } catch {
      setIsAuthenticated(false);
    } finally {
      setIsLoading(false);
    }
  };

  const logout = async () => {
    try {
      await axiosClient.post('auth/logout');
    } finally {
      setUser(null);
      setIsAuthenticated(false);
    }
  };

  useEffect(() => {
    setUnauthorizedHandler(() => setIsAuthenticated(false));
    checkAuth();
  }, []);

  return (
    <AuthContext.Provider value={{ isAuthenticated, user, isLoading, checkAuth, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};