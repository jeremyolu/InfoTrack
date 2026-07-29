export default interface AuthContextType {
  isAuthenticated: boolean;
  user: User;
  isLoading: boolean;
  checkAuth: () => Promise<void>;
  logout: () => Promise<void>;
}