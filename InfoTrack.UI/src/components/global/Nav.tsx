import { useAuth } from '../../contexts/AuthContext';

import '../../styles/components/Nav.css';

export default function Nav() {

  const { user, logout } = useAuth();

  const getFirstName = () => {
    const username= user?.username ?? "";
    const firstName = username.split(".")[0];
    return username ? `Hi ${firstName}!` : "Hi User";
  };

  return (
    <div className="navbar-container">
      <div className="navbar-left">
        <h1 className="navbar-title">InfoTrack</h1>
      </div>
      <div className="navbar-right">
        <div className='navbar-right-inner'>
          <p className='navbar-username'>{getFirstName()}</p>
          <button className="button button-white" onClick={logout} >Logout</button>
        </div>
      </div>
    </div>
  );
}