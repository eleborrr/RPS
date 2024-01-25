import React from 'react';

const Header = () => {
  return (
    <header style={headerStyle}>
      <div>
        <button style={buttonStyle}>Все игры</button>
        <button style={buttonStyle}>Рейтинг</button>
        <button style={buttonStyle}>Профиль</button>
      </div>
    </header>
  );
};

const headerStyle = {
  background: '#57477e', // цвет фона (фиолетовый)
  color: 'white', // цвет текста (белый)
  padding: '10px', // отступы внутри заголовка
  'box-shadow': '0 5px 0 rgba(0, 0, 0, 0.3)'
};

const buttonStyle = {
  background: 'black', // цвет фона кнопок (черный)
  color: 'white', // цвет текста кнопок (белый)
  margin: '0 5px', // отступы между кнопками
  border: 'none', // убираем границу кнопок
  'border-radius': '20px',
  padding: '5px 10px', // отступы внутри кнопок
  cursor: 'pointer', // указываем, что курсор должен меняться при наведении на кнопку
};

export default Header;
