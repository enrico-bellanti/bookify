import logo from '../../../assets/bookify_logo.png';

export function NavBar(){
    return (
        <div className='bg-purple-500 py-4 md:px-10 h-26 px-4 fixed top-0 left-0 right-0 z-10'>
            <div className="header-top flex justify-between items-center">
                <img src={logo} alt="bookyfy logo" className="w-30"/>
            </div>
            <div className="header-bottom flex justify-between items-center">
                <ul className='mt-4 flex gap-4'>
                    <li>Item</li>
                    <li>Item</li>
                    <li>Item</li>
                </ul>
            </div>
        </div>
    )
}