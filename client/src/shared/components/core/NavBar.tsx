import { Link } from 'react-router-dom';
import logo from '../../../assets/bookify_logo.png';
import { useAuth } from '../../components/auth/AuthContext';
import { ShowToAuthenticated, ShowToRoles, ShowToUnauthenticated } from '../../components/auth/ConditionalUI';

export function NavBar() {
  const auth = useAuth();

  return (
    <div className='bg-purple-500 py-4 md:px-10 h-26 px-4 fixed top-0 left-0 right-0 z-10'>
      <div className="header-top flex justify-between items-center">
        <img src={logo} alt="bookyfy logo" className="w-30" />
        
        <div className="flex items-center gap-4">
          <ShowToAuthenticated>
            <span className="text-white">Benvenuto, {auth.user?.profile.preferred_username || 'utente'}</span>
            <button 
              onClick={() => auth.logout()} 
              className="bg-white text-purple-700 px-4 py-2 rounded hover:bg-purple-100"
            >
              Logout
            </button>
          </ShowToAuthenticated>
          
          <ShowToUnauthenticated>
            <button 
              onClick={() => auth.login()} 
              className="bg-white text-purple-700 px-4 py-2 rounded hover:bg-purple-100"
            >
              Accedi
            </button>
          </ShowToUnauthenticated>
        </div>
      </div>
      
      <div className="header-bottom flex justify-between items-center">
        <ul className='mt-4 flex gap-4'>
          <li><Link to="/" className="text-white hover:text-purple-200">Home</Link></li>
          
          <ShowToAuthenticated>
            <li><Link to="/dashboard" className="text-white hover:text-purple-200">Dashboard</Link></li>
            <li><Link to="/profile" className="text-white hover:text-purple-200">Profilo</Link></li>
          </ShowToAuthenticated>
          
          <ShowToRoles roles={["admin"]}>
            <li><Link to="/admin" className="text-white hover:text-purple-200">Admin</Link></li>
          </ShowToRoles>
          
          <ShowToRoles roles={["editor"]}>
            <li><Link to="/editor" className="text-white hover:text-purple-200">Editor</Link></li>
          </ShowToRoles>
        </ul>
      </div>
    </div>
  );
}