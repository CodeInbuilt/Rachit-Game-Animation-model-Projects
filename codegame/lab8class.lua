-- Decision Tree Function
function classifyFruit(color, size)
    
    -- Root decision (color)
    if color == "orange" then
        return "Orange 🍊"
        
    elseif color == "red" then
        if size == "medium" or size == "small" then
            return "Apple 🍎"
        else
            return "Unknown Fruit"
        end
        
    elseif color == "green" then
        if size == "small" then
            return "Apple 🍎"
        else
            return "Unknown Fruit"
        end
        
    else
        return "Unknown Fruit"
    end
end

-- Test examples
print(classifyFruit("orange", "medium")) -- Orange
print(classifyFruit("red", "medium"))    -- Apple
print(classifyFruit("green", "small"))   -- Apple
print(classifyFruit("yellow", "large"))  -- Unknown