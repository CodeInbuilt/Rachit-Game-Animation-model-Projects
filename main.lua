-- main.lua (Love2D)

-- runs once at start
function love.load()
    -- set window title 
   love.window.setTitle("Display Gaming name on the screen in Lua")
end

-- runs every frame
function love.draw()
    -- set text color
    love.graphics.setColor(1, 1, 1)

    -- display game name on screen
    love.graphics.print("Welcome to Dragon Adventure Game", 180, 200, 0, 2, 2)
end